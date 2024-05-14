"use client";

import { ProfessionsSkills } from "../components/ProfessionsSkills";
import {
  getAllProfessionsWithSlug,
} from "../services/professions";
import { useEffect, useState } from "react";
import { Loading } from "../components/Loading";

export default function ProfessionsPage() {
  const [professions, setProfessions] = useState<ProfessionWithSlug[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const getProfessions = async () => {
        const professions = await getAllProfessionsWithSlug();

        setLoading(false);
        setProfessions(professions);
      };

    getProfessions();
  }, []);

  return (
    <div>
      {loading ? (
        <Loading></Loading>
      ) : (
        <ProfessionsSkills professions={professions} />
      )}
    </div>
  );
}


