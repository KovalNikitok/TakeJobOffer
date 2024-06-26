"use client"

import { useEffect, useState } from "react";

import { Loading } from "../../../shared/ui/Loading";
import { getProfessionBySlug } from "../../../shared/api/professions";
import { getProfessionSkillsWithNames } from "../../../shared/api/professionSkills";

import { ProfessionSkills } from "./ProfessionSkills";

interface ProfessionSkillsProps {
  slug:  string;
}

export default function ProfessionSkillsClient({ slug }: ProfessionSkillsProps) {
    const [professionSkills, setProfessionSkills] = useState<ProfessionSkillWithName[]>([]);
    const [profession, setProfession] = useState<Profession>({} as Profession);
    const [loading, setLoading] = useState(true);
  
    useEffect(() => {
      const getSkillsByProfessionSlug = async () => {
          const professionWithSlug = await getProfessionBySlug(slug);
          const professionSkillsFetched = await getProfessionSkillsWithNames(professionWithSlug.id, true);
          
          setProfession(professionWithSlug);
          setProfessionSkills(professionSkillsFetched);
          setLoading(false);
      }

      getSkillsByProfessionSlug();
    }, [slug]);
  
    return (
      <div>
        {
          loading ? (
            <Loading></Loading>
          ) : (
            <ProfessionSkills values={professionSkills} profession={profession}/>
          )
        }
      </div>
    );
}